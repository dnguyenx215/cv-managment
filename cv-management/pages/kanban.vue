<script setup>
    definePageMeta({
        middleware: 'auth'
    })
</script>

<template>
    <the-kanban-table></the-kanban-table>
</template>
