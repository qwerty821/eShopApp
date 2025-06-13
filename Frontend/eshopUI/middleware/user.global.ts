export default defineNuxtRouteMiddleware(async () => {
    if(import.meta.client) {
        const userStore = useUserStore();
        await userStore.fetchUser(); 
    }
})