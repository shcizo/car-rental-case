<template>
    <UHorizontalNavigation :links="links" class="border-b border-gray-200 dark:border-gray-800 mx-auto text-center" />
    <NuxtPage />
</template>
<script setup lang="ts">
const links = [
    [{
        label: 'Dealership',
        icon: 'i-material-symbols-directions-car',
        to: '/dealership'
    }],
    [{
        label: 'Handout car',
        icon: 'i-material-symbols-directions-car',
        to: '/handout'
    }, {
        label: 'Return car',
        icon: 'i-ion-md-return-left',
        to: '/return'
    }, {
        label: 'Setup',
        icon: 'i-ion-md-return-left',
        to: '/setup'
    }], []]
</script>
