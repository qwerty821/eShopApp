<script setup lang="ts">



definePageMeta({
  layout: false,
});

import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import {ref} from 'vue';
import { tr } from '@nuxt/ui-pro/runtime/locale/index.js';

import {useAuthStore } from '@/stores/auth.store';
import { error } from '#build/ui-pro';
const toast = useToast()

const fields = [{
	name: 'email', 
	type: 'text' as const,
	label: 'Email',
	placeholder: 'Enter your email',
	required: true,
	// value: "email@mail.com"
}, {
	name: 'password',
	label: 'Password',
	type: 'password' as const,
	placeholder: 'Enter your password',
	// value: "Password123@"
}, {
	name: 'remember',
	label: 'Remember me',
	type: 'checkbox' as const
}]

const providers = [{
	label: 'Google',
	icon: 'i-simple-icons-google',
	onClick: () => {
		toast.add({ title: 'Google', description: 'Login with Google' })
	}
}, {
	label: 'GitHub',
	icon: 'i-simple-icons-github',
	onClick: () => {
		toast.add({ title: 'GitHub', description: 'Login with GitHub' })
	}
}]

const schema = z.object({
	email: z.string().email('Invalid email'),
	password: z.string().min(6, 'Must be at least 8 characters')
})

type Schema = z.output<typeof schema>

const authError = ref({
	value: false,
	err: ''
});
const alertStore = useAlertStore();
const { alert } = storeToRefs(alertStore);
const authStore = useAuthStore();

async function onSubmit(user: FormSubmitEvent<Schema>) {
	alertStore.clear();
	
	const credentials = { email: user.data.email, password: user.data.password };

	try {
		const status = await authStore.login(credentials);
		if(status == 200) {
			navigateTo('/');
		}
	} catch(err) {
		
	}
}

async function validateToken() {
	await authStore.validateToken();
}
 

</script>

<template>
	<div class="flex flex-col items-center justify-center gap-4 p-4">
		<UPageCard class="w-full max-w-md">
			<UAuthForm :schema="schema" :fields="fields" :providers="providers" title="Welcome back!" 
				icon="i-lucide-lock" @submit.prevent="onSubmit">
				<template #description>
					Don't have an account? <ULink to="/accounts/register" class="text-(--ui-primary) font-medium">Sign up</ULink>.
				</template>
					
				<template #password-hint>
					<NuxtLink to="/" class="text-(--ui-primary) font-medium">Forgot password?</NuxtLink>
				</template>
				<template #validation>
					<UAlert v-if="alert" color="error" icon="i-lucide-info" :title="alert.message" />
				</template>
				<template #footer>
					By signing in, you agree to our <ULink to="#" class="text-(--ui-primary) font-medium">Terms of
						Service</ULink>.
				</template>
			</UAuthForm>
		</UPageCard>
		<UButton @click="validateToken">AAAAA</UButton>
	</div>
</template>
