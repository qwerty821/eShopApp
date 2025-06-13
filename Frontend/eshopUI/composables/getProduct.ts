export const useGetProduct = (slug: string) => {

  

	const config = useRuntimeConfig()
	const key = `product-${slug}`

	return useAsyncData<IProduct[]>(key, async () => {
		try {
			const response = await $fetch<IProduct[]>(`${config.public.productsUrl}/${slug}`)
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
