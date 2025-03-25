<template>
	<div>
		<div class="parent">
			<div class="div1">
				<UPageCard
					title="Tailwind CSS"
					description="Nuxt UI v3 integrates with latest Tailwind CSS v4, bringing significant improvements."
					icon="i-simple-icons-tailwindcss"
					orientation="horizontal"
					reverse
				>
					<img src="https://ui.nuxt.com/tailwindcss-v4.svg" alt="Tailwind CSS" class="w-full" />
				</UPageCard>
			</div>
			<div class="div2">
				<UPageCard
					title="Tailwind CSS"
					description="Nuxt UI v3 integrates with latest Tailwind CSS v4, bringing significant improvements."
					icon="i-simple-icons-tailwindcss"
					orientation="horizontal"
					reverse
				>
					<img src="https://ui.nuxt.com/tailwindcss-v4.svg" alt="Tailwind CSS" class="w-full" />
				</UPageCard>
			</div>
			<div class="div3">
				<UPageCard 
					title="Tailwind CSS"
					description="Nuxt UI v3 integrates with latest Tailwind CSS v4, bringing significant improvements."
					icon="i-simple-icons-tailwindcss"
					to="https://tailwindcss.com/docs/v4-beta"
					target="_blank"
					variant="outline"
  				/>
			</div>
			<div class="div4">
				<UPageCard
					title="Tailwind CSS"
					description="Nuxt UI v3 integrates with latest Tailwind CSS v4, bringing significant improvements."
					icon="i-simple-icons-tailwindcss"
					to="https://tailwindcss.com/docs/v4-beta"
					target="_blank"
					variant="outline"
				/>
			</div>
			<div class="div5">
				<UPageCard
					title="Tailwind CSS"
					description="Nuxt UI v3 integrates with latest Tailwind CSS v4, bringing significant improvements."
					icon="i-simple-icons-tailwindcss"
					to="https://tailwindcss.com/docs/v4-beta"
					target="_blank"
					variant="outline"
				/>
			</div>
		</div>
	</div>
</template>

<script lang="ts" setup>

</script>

<style scoped>

.parent {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-template-rows: repeat(3, 1fr);
    gap: 10px;
}
    
.div1 {
    grid-column: span 3 / span 3;
}

.div2 {
    grid-column: span 3 / span 3;
    grid-row-start: 2;
}

.div3 {
    grid-row-start: 3;
}

.div4 {
    grid-row-start: 3;
}

.div5 {
    grid-row-start: 3;
}
        
</style>