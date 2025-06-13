<script setup>
import { storeToRefs } from 'pinia';

import { useAlertStore } from '@/stores/alert.store';

const alertStore = useAlertStore();
const { alert } = storeToRefs(alertStore);
</script>

<template>
	<div v-if="alert" class="container">
		<!-- <UBanner id="example" :title="alert.message" @close.prevent="alertStore.clear()"/> -->
		<UBanner id="example" :title="alert.message" close />
	</div>
</template>