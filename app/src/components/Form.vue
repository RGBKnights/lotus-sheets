<template>
  <FormKit type="form" @submit="submitHandler">
    <FormKitSchema :schema="schema" />
  </FormKit>
</template>

<script setup lang="ts">
import { FormKit, FormKitSchema } from '@formkit/vue'
import { ref } from 'vue';

const schema = ref([
  {
    $el: 'h1',
    children: 'Register'
  },
  {
    $formkit: 'text',
    name: 'email',
    label: 'Email',
    help: 'This will be used for your account.',
    validation: 'required|email'
  },
  {
    $formkit: 'password',
    name: 'password',
    label: 'Password',
    help: 'Enter your new password.',
    validation: 'required|length:5,16'
  },
  {
    $formkit: 'password',
    name: 'password_confirm',
    label: 'Confirm password',
    help: 'Enter your new password again to confirm it.',
    validation: 'required|confirm',
    validationLabel: 'password confirmation',
  },
  {
    $formkit: 'checkbox',
    id: 'eu',
    name: 'eu_citizen',
    label: 'Are you a european citizen?',
  },
  {
    $formkit: 'select',
    if: '$get(eu).value', // 👀 Oooo, conditionals!
    name: 'cookie_notice',
    label: 'Cookie notice frequency',
    options: {
      refresh: 'Every page load',
      hourly: 'Ever hour',
      daily: 'Every day'
    },
    help: 'How often should we display a cookie notice?'
  }
])

const submitHandler = async () => {
  await new Promise((r) => setTimeout(r, 1000))
  alert("Submitted")
}
</script>

<style>
</style>