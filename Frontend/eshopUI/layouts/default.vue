<template>
  <div class="min-h-screen grid grid-rows-[auto_1fr_auto]">
    <!-- Header -->
    <div class=" ">
      <NavBar />
    </div>

    <!-- Main Content -->
    <div class="grid grid-cols-[minmax(10px,0)_minmax(0,12fr)_minmax(10px,0)] md:grid-cols-[1fr_12fr_1fr]">
      <div class=" text-xs text-center"> </div>
      <div class="main-content">
        <slot></slot>
      </div>
      <div class=" text-xs text-center"> </div>
    </div>

    <!-- Footer -->
    <div class=" text-center">
    	 <USeparator />
		footer
    </div>
  </div>
</template>

<script setup lang="ts">
import NavBar from '~/components/NavBar.vue'
</script>

<style scoped>
.main-content{
	box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 12px;
}
</style>