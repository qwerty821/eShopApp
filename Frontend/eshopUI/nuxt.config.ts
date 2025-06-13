// https://nuxt.com/docs/api/configuration/nuxt-config

import tailwindcss from "@tailwindcss/vite";

console.log('API_BASE_URL_PRODUCTS:', process.env.API_BASE_URL_PRODUCTS);
export default defineNuxtConfig({
								ssr: true,
								devServer: {
																https: true
								},
								// alias: {
								// 	assets: "/<rootDir>/assets"
								// },
								compatibilityDate: '2024-11-01',
								pages: true,
								modules: [// '@nuxtjs/tailwindcss'
																'@nuxt/ui', // 'nuxt-headlessui',
																'@nuxtjs/color-mode', // '@sidebase/nuxt-auth',
																'@pinia/nuxt', '@vueuse/nuxt', '@nuxt/image', 'vue3-carousel-nuxt', 'pinia-plugin-persistedstate/nuxt'],

								css: ['~/assets/css/main.css'],

								devtools: { enabled: false },

								ui: {
																colorMode: false
								},

								runtimeConfig: {
																public: {
																								jwksUrl: process.env.JWKS_BASE_URL,
																								nightMode: process.env.NIGHT_MODE,
																								productsUrl: process.env.API_BASE_URL_PRODUCTS,
																								imagesUrl: process.env.API_BASE_URL_STATIC_CONTENT,
																								uploadImageUrl: process.env.API_IMAGES_UPLOAD,
																								// productsUrl: '/api/catalog',
																								// imagesUrl: '/api/images',
																								// uploadImageUrl: '/api/upload',
																								userLoginUrl: process.env.USER_LOGIN_URL,
																								userLogOutUrl: process.env.USER_LOGOUT_URL,
																								userRegisterUrl: process.env.USER_RGISTER_URL,
																								userProfileUrl: process.env.USER_PROFILE_URL

																}
								},
								nitro: {

																// devProxy: {
																// 	'/api/catalog': {
																// 		target: process.env.API_BASE_URL_PRODUCTS,
																// 		changeOrigin: true,
																// 		prependPath: false
																// 	},
																// 	'/api/images': {
																// 		target: process.env.API_BASE_URL_STATIC_CONTENT,
																// 		changeOrigin: true,
																// 		prependPath: false
																// 	},

																// 	'/api/upload': {
																// 		target: process.env.API_IMAGES_UPLOAD,
																// 		changeOrigin: true,
																// 		prependPath: false
																// 	}
																// }
								}
								// vue: {
								// 	compilerOptions: {
								// 	  isCustomElement: (tag) => ['UApp', 'UPageGrid'].includes(tag),
								// 	}
								//   },
								// debug: true


								// image: {
								// 	providers: {
								// 	  random: {
								// 		provider: '~/providers/random',
								// 		options: {}
								// 	  }
								// 	}
								//   }

								// vite: {
								// 	plugins: [
								// 	  tailwindcss(),
								// 	],
								//   },
								// aceaste optiuni sunt pentru autentificare sidebase-auth
								// runtimeConfig: {
								// 	baseURL: 'https://localhost:32081/api/'
								// },

								// auth: {
    // isEnabled: true,
								// baseURL: 'https://localhost:30111',
    // provider: {
    //   type: 'local', // tipul de provider
    //   endpoints: {
    //     signIn: { path: '/api/auth/Login', method: 'post' },   
    //     signOut: { path: '/api/Users/Logout', method: 'post' }, 
    //     getSession: { path: '/api/Users/GetProfile', method: 'get' }  
    //   },
    //   token: {
    //     type: 'cookie', 
    //     cookieName: 'token', 
    //     cookieDomain: 'localhost',  
    //     sameSiteAttribute: 'none',
    //     secureCookieAttribute: true,
    //     httpOnlyCookieAttribute: true,
    //     maxAgeInSeconds: 60 * 60  
    //   },
    //   pages: {
    //     login: '/accounts/login' 
    //   }
    // }
//   }
})
