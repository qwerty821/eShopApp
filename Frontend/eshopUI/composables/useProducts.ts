export const fetchProducts = (page: Number, pageSize: Number) => {
	const config = useRuntimeConfig()
	const key = `all-products`

	return useAsyncData<PaginatedResultDTO<ProductDTO>>(key, async () => {
		try {
			const response = await $fetch<PaginatedResultDTO<ProductDTO>>(`${config.public.productsUrl}`, {
				query: {
					page,
					pageSize
				}}
			);
			
			return response
		} catch (error) {
			console.error('Error fetching products:', error)
			throw createError({
				statusCode: 500,
				message: 'Failed to fetch products'
			})
		}
	}, {
		server: true,  // cererea se executa doar pe server
		lazy: false,        // asteapta datele înainte de render
		
		transform: (data) => {
			if (import.meta.server) {
				console.log("Date pe server:", data);
			  } else {
				console.log("Date pe client:", data);
			  }
			  return data;
		}
	})
}


export function useProductList() {
  const products = ref([])
  const page = ref(1)
  const pageSize = ref(10)
  const totalPages = ref(0)
  const search = ref('')

  const fetchProducts = async () => {
    const res:any = await $fetch('/api/products', {
      query: { page: page.value, pageSize: pageSize.value, search: search.value }
    })
    products.value = res.data
    totalPages.value = res.totalPages
  }

  watch([page, pageSize, search], fetchProducts, { immediate: true })

  return { products, page, pageSize, totalPages, search }
}
