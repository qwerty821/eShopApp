// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: false },
  css: ["~/assets/css/main.css"],
  modules: [ "@nuxt/ui"],
   
  compatibilityDate: "2025-04-13",
  // tailwindcss: { exposeConfig: true },  
  
});

