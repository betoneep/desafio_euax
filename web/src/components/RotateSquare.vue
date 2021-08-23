<template>
  <div :style="styles" class="spinner spinner--rotate-square-2"></div>
</template>

<script>
export default {
  props: {
    size: {
      type: String,
      default: "40px"
    }
  },
  computed: {
    styles() {
      return {
        width: "20px",
        height: this.size
      };
    }
  }
};
</script>

<style lang="scss">
.loading-position {
  position: relative;
  margin: 2rem 0 2rem 0;
}

.loading-container {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  align-content: center;
  justify-content: center;
  margin: auto;
}

.spinner {
  position: relative;
  * {
    line-height: 0;
    box-sizing: border-box;
  }
  &:before {
    content: "";
    width: 100%;
    height: 20%;
    min-width: 5px;
    background: #000;
    opacity: 0.1;
    position: absolute;
    bottom: 0%;
    left: 0;
    border-radius: 50%;
    animation: rotate-square-2-shadow 0.5s linear infinite;
  }
  &:after {
    content: "";
    width: 100%;
    height: 100%;
    background: var(--primary); /* fallback for old browsers */
    animation: rotate-square-2-animate 0.5s linear infinite;
    position: absolute;
    bottom: 40%;
    left: 0;
    border-radius: 3px;
  }
}

@keyframes rotate-square-2-animate {
  17% {
    border-bottom-right-radius: 3px;
  }
  25% {
    transform: translateY(20%) rotate(22.5deg);
  }
  50% {
    transform: translateY(40%) scale(1, 0.9) rotate(45deg);
    border-bottom-right-radius: 50%;
  }
  75% {
    transform: translateY(20%) rotate(67.5deg);
  }
  100% {
    transform: translateY(0) rotate(90deg);
  }
}

@keyframes rotate-square-2-shadow {
  0%,
  100% {
    transform: scale(1, 1);
  }
  50% {
    transform: scale(1.2, 1);
  }
}
</style>
