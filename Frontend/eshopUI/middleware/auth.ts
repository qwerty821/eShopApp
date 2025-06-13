import { defineNuxtRouteMiddleware, navigateTo } from '#app'
import { useRequestHeaders } from 'nuxt/app'

export default defineNuxtRouteMiddleware(async (to) => {
    if (to.path === '/accounts/login') return
    const user = useUserStore();
    if (!user.isAuthenticated && import.meta.client) {
        return navigateTo('/accounts/login')
    }
})