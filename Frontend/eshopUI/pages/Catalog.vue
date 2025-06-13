<template>
  <div class="grid grid-cols-1 md:grid-cols-12 gap-4 p-4">
    <!-- Filtru -->
    <div class="md:col-span-2 border border-red-500 bg-blue-50">
    </div>

    <!-- produse -->
    <div class="md:col-span-10 border border-red-500 bg-green-100 px-4 py-8 w-full">
      <!-- sortare -->
      <div class="mb-6 flex flex-wrap gap-4">
        <button class="px-4 py-2 bg-gray-100 rounded-full">Toate</button>
        <button class="px-4 py-2 bg-gray-100 rounded-full">Reduceri</button>
      </div>

      
      <div v-if="products != null" class="grid gap-4 grid-cols-2 sm:grid-cols-3 lg:grid-cols-4">
        <ProductCard v-for="product in products" :key="product.id" :product="product" />
      </div>
      <div v-else>
        <h1 class="text-center">Nu exista produse</h1>
      </div>

      <!-- paginare -->
      <div class="mt-8 flex justify-center">
        <a-pagination
          v-model:current="currentPage"
          :page-size="pageSize"
          :total="totalPages * pageSize"
          show-less-items
        />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { fetchProducts } from '#imports'

const pageSize = 5
const currentPage = ref(1)
 
const pending = ref(false)
const error = ref()
 
const { data, refresh } = await useAsyncData(
  'products',
  async () => {
    const result = await fetchProducts(currentPage.value, pageSize)
    return result.data.value 
  }
)
const products = computed(() => data.value?.items || [])
const totalPages = computed(() => data.value?.totalPages || 1)

watch(currentPage, async () => {
  await refresh()
})

</script>

<style>
 
</style>