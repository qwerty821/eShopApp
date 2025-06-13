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

	// let token: string | null = null;
	const token = ref<string | null>(null);

	const config = useRuntimeConfig();
	const JWKS_URL = `${config.public.jwksUrl}`;

	const alertStore = useAlertStore();
	const jwks = createRemoteJWKSet(new URL(JWKS_URL));

	const isAuthenticated = ref(false);

	const cookieToken = useCookie('token');

	onMounted(() => {
		initToken();
	});

	watch(token, async (newToken) => {
		await checkAuthentication();
	});

	async function initToken() {
		if (!token.value && cookieToken.value) {
			token.value = cookieToken.value;
			// console.info("token init with " + token.value);
		}
		await checkAuthentication();
	}
	async function checkAuthentication() {
		isAuthenticated.value = await validateToken();
	}

	async function login(user: IUser): Promise<number | void> {
		if (import.meta.client) {
			try {
				const data = await $fetch<{ token: string }>(`${config.public.userLoginUrl}`, {
					method: 'POST',
					headers: {
						'Content-Type': 'application/json',
					},
					body: JSON.stringify(user),
				});

				token.value = data.token;
				cookieToken.value = token.value;

				await checkAuthentication();

				return 200;

			} catch (error: any) {
				alertStore.error(statusCodes[error.status] || 'Eroare necunoscuta');
			}
		}
	}

	async function validateToken(): Promise<boolean> {
		if (!token.value || typeof token.value !== "string") {
			// console.log("token null la verificare");			
			return false;
		}

		try {

			const { payload } = await jwtVerify(token.value, jwks);
			// console.log(payload);

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



	function logout(): boolean {
		token.value = null;
		cookieToken.value = null;
		isAuthenticated.value = false;
		return true;
	}
	return {
		token,
		isAuthenticated,
		login,
		logout,
		validateToken
	};
});
