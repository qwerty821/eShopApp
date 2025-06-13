import { Meta } from '#components';
import { message } from 'ant-design-vue'
import { defineStore } from 'pinia'
import type { IUser } from '~/interfaces/UserInterfaces'


export const useUserStore = defineStore('user', () => {
    const config = useRuntimeConfig()

    const user = ref<IUser| null>(null)

    const isAuthenticated = computed(() => user.value !== null && user.value !== undefined)
    const isUserLoaded = ref(false)

    const setUser = (u: IUser) => {
        user.value = u;
    }

    async function clearUser() {
        await $fetch(config.public.userLogOutUrl,  { method: "POST", credentials: 'include' });
        user.value = null
        isUserLoaded.value = false
    }

  
    async function fetchUser(force = false) {
        if (isUserLoaded.value && !force) return

        try {
            const data = await $fetch(config.public.userProfileUrl, {
                credentials: 'include',
                headers: import.meta.server ? useRequestHeaders(['cookie']) : undefined
            })
            const userData: IUser =  lowercaseKeys(data);
            setUser(userData)
        } catch (err) {
            clearUser()
        } finally {
            isUserLoaded.value = true
        }
    }

    async function login(userDto: IUserLoginDTO) {
        try {
            const data = await $fetch(config.public.userLoginUrl, {
                method: 'POST',
                body: userDto,
                credentials: 'include'
            });
            fetchUser(true);  
            return { success: true, message: "succes"};
        } catch {
            return { success: false, message:"eroare la autentificare"}
        }
    }

    return {
        user,
        isAuthenticated,
        setUser,
        clearUser,
        fetchUser,
        login
    }
},
{
// persist: {
//     storage: sessionStorage,
//     pick: [],
//   },
}
 );

 