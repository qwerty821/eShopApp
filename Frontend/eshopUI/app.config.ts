export default defineAppConfig({
	ui: {
		button: {
			slots: {
				base: 'font-bold ',
			}
		}
	},
	uiPro: {
		pageBody: {
			// base: 'mt-8 pb-24 space-y-12'
		},
		 
		page: {
			compoundVariants: [
				{
				  left: true,
				  right: true,
				  class: {
					center: 'lg:col-span-8'
				  }
				} 
			  ]
		}
	}
})