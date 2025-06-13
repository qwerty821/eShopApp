<script setup lang="ts">

definePageMeta({
  layout: false,
});

import * as z from 'zod'
import type { FormSubmitEvent } from '@nuxt/ui'
import {ref} from 'vue';

const toast = useToast()

const fields = [
{
	name: 'username', 
	type: 'text' as const,
	label: 'username',
	placeholder: 'Enter your username',
	required: true,
	value: "testusername"
},
{
	name: 'email', 
	type: 'text' as const,
	label: 'Email',
	placeholder: 'Enter your email',
	required: true,
	value: "email@mail.com"
}, {
	name: 'password',
	label: 'Password',
	type: 'password' as const,
	placeholder: 'Enter your password',
	value: "Password123@"
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
  username: z.string().min(5, 'Must be at least 5 characters'),
	email: z.string().email('Invalid email'),
	password: z.string().min(8, 'Must be at least 8 characters')
})

type Schema = z.output<typeof schema>

const authError = ref(false);

 

const username = ref('')
const password = ref('')


function onSubmit(payload: FormSubmitEvent<Schema>) {
	// console.log("email = ",payload.data.email);
	// signIn({schema., password})
	authError.value = true;
}


</script>

<template>
	<div class="flex flex-col items-center justify-center gap-4 p-4">
		<UPageCard class="w-full max-w-md">
			<UAuthForm :schema="schema" :fields="fields" :providers="providers" title="Welcome back!" 
				icon="i-lucide-lock" @submit.prevent="onSubmit">
				<template #description>
					Have an account? <ULink to="/accounts/login" class="text-(--ui-primary) font-medium">Log in</ULink>.
				</template>
					
				<template #password-hint>
					<NuxtLink to="/" class="text-(--ui-primary) font-medium">Forgot password?</NuxtLink>
				</template>
				<template #validation>
					<UAlert v-if="authError" color="error" icon="i-lucide-info" title="Error signing in" />
				</template>
				<template #footer>
					By signing in, you agree to our <ULink to="#" class="text-(--ui-primary) font-medium">Terms of
						Service</ULink>.
				</template>
			</UAuthForm>
		</UPageCard>
	</div>
</template>
