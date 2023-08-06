<script setup>
import { onMounted } from 'vue';
import { useExampleStore } from '@/stores/example';
onMounted(async () => {
  const { example } = useExampleStore();
  await example();
})
</script>

<template>
  <div>
  </div>
</template>

<style>
</style>
