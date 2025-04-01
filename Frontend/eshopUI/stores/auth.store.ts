import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import { useRuntimeConfig } from '#app';
import { useAlertStore } from '@/stores/alert.store';
import { useRouter } from 'vue-router';

import type { IUser } from '~/interfaces/UserInterfaces';
import { boolean, string } from 'zod';

import { jwtVerify, createRemoteJWKSet } from 'jose';

const statusCodes: Record<number, string> = {
	401: 'Acest utilizator nu exista',
};


export const useAuthStore = defineStore('auth-store', () => {
 
	let token: string | null = null;
	if (import.meta.client) {
		token = localStorage.getItem('token');
		console.info("token init with " + token);
	}
	const config = useRuntimeConfig();
	const JWKS_URL = `${config.public.jwksUrl}`;

	const alertStore = useAlertStore();
	const jwks = createRemoteJWKSet(new URL(JWKS_URL));
	
	const isAuthenticated = computed(() => !!token);

	async function login(user: IUser): Promise<number | void> {
		try {
			const data = await $fetch<{ token: string }>(`${config.public.apiBaseUrl}/auth/login`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify(user),
			});

			token = data.token;
			localStorage.setItem('token', data.token);

			return 200;

		} catch (error: any) {
			alertStore.error(statusCodes[error.status] || 'Eroare necunoscuta');
		}
	}

	async function validateToken(): Promise<boolean> {
		try {
			console.log("init " + token);
			if (!token) {
				console.log("token null");
				return false; 
			}
			const { payload } = await jwtVerify(token, jwks);
			console.log(payload);
			
			const now = Math.floor(Date.now() / 1000);
			if (payload.exp && payload.exp < now) {
				return false; // Token expirat
			}
			return true;  
		} catch (error) {
			console.error("eroare : " + error);
			return false;  
		}
	}

	return {
		token,
		isAuthenticated,
		login,
		validateToken
	};
});
