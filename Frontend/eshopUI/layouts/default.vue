<template>
	<div>
		<header>
			<PageHeaderMenu>
				<slot name="header"></slot>
			</PageHeaderMenu>
		</header>
		<main >
			<UPage>
				<template #left>
					<UPageAside class="lg:col-span-1"/>
				</template>

				<UPageBody>
					<slot ></slot>
				</UPageBody> 

				<template #right>
					<UPageAside class="lg:col-span-1" />
				</template>

			</UPage>
		</main>
		
		<footer>
			<PageFooter>
				<slot name="footer"></slot>
			</PageFooter>
		</footer>
		 
	</div>
</template>

<script lang="ts" setup>

</script>

<style scoped>
	 
</style>