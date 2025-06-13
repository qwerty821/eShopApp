
import { defineStore } from 'pinia'

export const useAlertStore = defineStore('alert', () => {
	const alert = ref<null | { message: string, type: string }>(null)

	function success(message: string) {
		alert.value = { message, type: 'alert-success' }
	}

	function error(message: string) {
		alert.value = { message, type: 'alert-danger' }
	}

	function clear() {
		alert.value = null
	}

	return { alert, success, error, clear }
})
