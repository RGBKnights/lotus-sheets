// import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useExampleStore = defineStore('counter', () => {
  async function example() {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/example`
    const response = await fetch(url)
    const weather = await response.json()
    console.log(weather)
  }

  return { example }
})
