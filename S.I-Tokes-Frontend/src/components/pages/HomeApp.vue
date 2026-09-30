<template>
    <div id="logo" class="pa-1 position-absolute" style="top: 50%; left: 50%; transform:translate(-50%, -50%)">
        <img src="/logo.png" width="400"/>
    </div>
</template>

<script>
export default {
    
}
</script>

<style scoped>
@keyframes slideInFromRight {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

#logo {
  animation: slideInFromRight 2s;
}

</style>