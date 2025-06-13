<script setup lang="js">
 
// async function show() {

// 	const auth = useAuthStore();
// 	const profileData = await $fetch('https://localhost:32081/api/Users/GetProfile', {
// 		method: 'GET',
// 		headers: {
// 			"Authorization": `Bearer ${auth.token}`
// 		}
		 
//  	});
// 	console.log(profileData);
// }


// const auth = useAuthStore();

// onMounted(async () => {
//   const profileData = await $fetch('https://localhost:32081/api/Users/GetProfile', {
//     headers: {
//       "Authorization": `Bearer ${auth.token}`
//     }
//   });
//   console.log(profileData);
// });

</script>

<template>
    <div>
        <UButton> <ULink to ="addproduct">Add Product</ULink></UButton>

     	<ClientOnly fallbackTag="h1">
            ASDasdasd
        </ClientOnly>
        
    </div>
</template>


<style scoped>
 
</style>