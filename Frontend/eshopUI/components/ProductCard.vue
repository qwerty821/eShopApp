<script lang="ts" setup>
import { da } from '@nuxt/ui/runtime/locale/index.js';

console.log ("din produs card");

const prop = defineProps({
	product: {
		type: Object as PropType<ProductDTO>,  
		required: true,
	},
});
const config = useRuntimeConfig();

const item = computed(() => normalizeProduct(prop.product));
function normalizeProduct(data: ProductDTO): ProductDTO {
	return {
		id: data.id,
		name: String(data.name || "Produs necunoscut"),
		price: Number(data.price) || 0,
		image: data.image,
		slug: data.slug,
		rating: data.rating,
		discount: data.discount
	};
}

</script>
<template>
	<div class="w-full bg-white rounded-lg shadow-md overflow-hidden hover:shadow-xl transition-shadow duration-300 h-full flex flex-col">
			<ULink :to=item.slug>

		<div class="relative aspect-square flex items-center justify-center ">
				<img :src="item.image" :alt="item.name" class="max-w-[70%] max-h-[70%] object-contain" />
			<!-- Reducere -->
			<span v-if="item.discount > 0"
				class="absolute top-2 right-2 bg-red-500 text-white text-xs font-bold px-2 py-1 rounded-full">
				-{{ item.discount }}%
			</span>
		</div>
			</ULink>

		<!-- Detalii produs -->
		<div class="p-4 flex-grow">
			<h3 class="text-lg font-semibold text-gray-800 mb-1 line-clamp-2">{{ item.name }}</h3>
			<div v-if="product" class="flex items-center mb-2">
				<span class="text-yellow-400">★</span>
				<span class="text-sm text-gray-600 ml-1">{{ item.rating }} (32)</span>
			</div>

			<!-- Pret -->
			<div class="mt-auto">
				<div class="flex items-center justify-between">
					<div>
						<span class="text-lg font-bold text-gray-900">
							{{ ((item.price) * (1 - item.discount / 100)).toFixed(2) }} lei

						</span>
						<span v-if="item.discount > 0" class="text-sm text-gray-500 line-through ml-2">
							{{ item.price }} lei
						</span>
					</div>
					<button class="bg-blue-600 hover:bg-blue-700 text-white p-2 rounded-full transition">
						<svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24"
							stroke="currentColor">
							<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
								d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
						</svg>
					</button>
				</div>
			</div>
		</div>
	</div>
</template>