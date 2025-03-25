<script setup lang="js">
const cards = ref([
	{
		title: 'Icons',
		description: 'Nuxt UI integrates with Nuxt Icon to access over 200,000+ icons from Iconify.',
		icon: 'i-lucide-smile',
		to: '/getting-started/icons'
	},
	{
		title: 'Fonts',
		description: 'Nuxt UI integrates with Nuxt Fonts to provide plug-and-play font optimization.',
		icon: 'i-lucide-a-large-small',
		to: '/getting-started/fonts'
	},
	{
		title: 'Color Mode',
		description: 'Nuxt UI integrates with Nuxt Color Mode to switch between light and dark.',
		icon: 'i-lucide-sun-moon',
		to: '/getting-started/color-mode'
	}
])

async function show() {

	console.log("aaaaaaa");
	const profileData = await $fetch('https://localhost:32081/api/Users/GetProfile', {
		method: 'GET',
		headers: {
			"Authorization": "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjFiZWFlNWM0LTc3MjAtNDliZi1iOTgzLWNjYjc4MjNlOTFmMSIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwianRpIjoiZDU3YTU3ZTMtYjM0Ny00MjNiLWJjODEtMDAyZDhiOTg2ZjZlIiwiZ2l2ZW5fbmFtZSI6InN0cmluZyIsIm5pY2tuYW1lIjoidXNlckBleGFtcGxlLmNvbSIsImVtYWlsIjoidXNlckBleGFtcGxlLmNvbSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE3NDI5NDA0NDIsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjgwODEiLCJhdWQiOiJ3ZWItZXNob3AifQ.SlILSQjl7J4JLKE7O-ao2mA3D3ecn5CgtzFchkZZCF4VfAWkDxNbhWyNWIkahQkqbjwdBTzyVNG14iEpQrr_OE13To-yZZj70hkqLfr6cwNOF98i8X9NtGV-mDKBBRbo2UmVcog4tEx6jqAU6j1-uyftO7fypCVrfyJ-QtbW52bUVcol2Nh8Hga796MyIkcn4S6PyLs2NqlwT0y0OwVXm9CcMzaBeybg6so5Tyj7fQ4N9J_S0azAfxAN2kIL-EXQTzTv47HAFkWQDeK9feA3cLy_kcviLa4BQShZMGwTNvD5BVyoflyYNx_DBNsYRscICV_SbppeVmJ5vdU7jbQhPQ"
		}
		 
 	});
	console.log(profileData);
}
</script>

<template>
	<UPageGrid>
		<UPageCard v-for="(card, index) in cards" :key="index" v-bind="card" />
	</UPageGrid>

	<UButton @click="show">Cliddck Me</UButton>
</template>


<style scoped>
UPageGrid {
	height: 300px;
}
</style>