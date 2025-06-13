<template>
	<div>
		<client-only>
			<div v-if="isAuthenticated" class="hidden sm:flex sm:items-center">
				<a-avatar style="background-color: #87d068">
					<template #icon>
						<UserOutlined />
					</template>
				</a-avatar>
				<a-divider type="vertical" />
				<a-dropdown>
					<a class="ant-dropdown-link" @click.prevent>
						{{ user?.firstname }}
						<DownOutlined />
					</a>
					<template #overlay>
						<a-menu>
							<a-menu-item>
								<NuxtLink to="/accounts/profile">Profil</NuxtLink>
							</a-menu-item>
							<a-menu-item>
								<a href="javascript:;">1.--------------</a>
							</a-menu-item>
							<a-menu-item>
								<a href="javascript:;">2.---------------</a>
							</a-menu-item>
							<a-menu-item>
								<a href="javascript:;">3.---------------</a>
							</a-menu-item>
							<a-menu-item>
								<div v-on:click="logoutEvent" 
									class="text-center"  style="margin: 5px auto; ">
									Log Out
								</div>
							</a-menu-item>
						</a-menu>
					</template>
				</a-dropdown>

			</div>
			<div v-else class="hidden sm:flex sm:items-center">
				<NuxtLink to="/accounts/login"
					class="text-gray-900 hover:bg-gray-100 px-3 py-2 rounded-md text-sm font-medium">
					Login
				</NuxtLink>
				<NuxtLink to="/accounts/register"
					class="ml-4 bg-indigo-600 text-white px-4 py-2 rounded-md text-sm font-medium hover:bg-indigo-700">
					Sign Up
				</NuxtLink>
			</div>
		</client-only>
	</div>
</template>

<script lang="ts" setup>



import { UserOutlined } from '@ant-design/icons-vue';

const userStore = useUserStore();

onMounted(async () => {
	await userStore.fetchUser();
});


const { isAuthenticated, user } = storeToRefs(userStore);

console.log(user.value?.firstname);

function logoutEvent() {
	userStore.clearUser();
	navigateTo("/");

}
</script>

<style></style>