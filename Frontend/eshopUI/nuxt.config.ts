// https://nuxt.com/docs/api/configuration/nuxt-config

import tailwindcss from "@tailwindcss/vite";	

export default defineNuxtConfig({
	// alias: {
	// 	assets: "/<rootDir>/assets"
	// },
	compatibilityDate: '2024-11-01',
	pages: true,
	// modules: ['@nuxt/ui'],
	modules: [
		'@nuxt/ui-pro', 
		'@nuxtjs/color-mode', 
		'nuxt-headlessui',
		// '@nuxtjs/tailwindcss'
	],

	css: ['~/assets/css/main.css'],
	 
	devtools: { enabled: false },

	ui: {
		colorMode: true
	},
	 
})
