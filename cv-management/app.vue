<template>
    <div>
        <NuxtLayout>
            <NuxtPage />
        </NuxtLayout>
    </div>
</template>
<script setup>
definePageMeta({
    layout: 'login',
    middleware: 'auth'
});
</script>
