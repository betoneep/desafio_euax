<template>
  <div class="search-box">
    <i class="search-icon fa fa-search"></i>
    <input
      v-model="inputText"
      class="search"
      type="text"
      :placeholder="this.$props.placeHolder"
      @keyup.enter="onSearch"
    />
  </div>
</template>

<script>
export default {
  name: "SearchBox",
  props: {
    placeHolder: {
      type: String,
      default: "Busque aqui"
    },
    emit: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      inputText: ""
    };
  },
  mounted() {},
  methods: {
    onSearch() {
      this.$emit(this.$props.emit, this.inputText);
    }
  }
};
</script>

<style lang="scss" scoped>
.search-box {
  display: flex;
  flex-direction: row;
  //   display: inline-block;
  width: 15rem;
  height: 3rem;
  //   background: #6a6c75;
  // border-radius: 5px;
  // border-bottom: 1px solid #ffff;
  transition: all 0.25s ease-in 0.25s;
}

.search {
  z-index: 101;
  width: 13rem;
  height: 2rem;
  content: "";
  border-radius: 5px;
  background: transparent;
  border: none;
  // color: transparent;
  color: rgba(255, 255, 255, 0.938);
  text-align: left;
  font-size: 14px;
  transition: all 0.25s ease-in 0.25s;
  background: rgba(255, 255, 255, 0.425);

  &:hover,
  &:focus {
    cursor: pointer;
    // width: 90%;
    // height: (0.5 + 2rem);
    // padding-right: 2rem;
    //
    border: 1px solid rgba(82, 99, 255, 0.815);
    border-radius: 5px;
    background: #6a6c75;
    color: #e9e9e9;
    transition: all 0.25s ease-in 0.25s;
    &:focus {
      cursor: text;
    }
    + .search-icon {
      //   color: rgb(56, 56, 151);
      // margin-left: -20px;
    }
    + .search-box {
      //   background: #6a6c75;
    }
  }
}

.fa-search {
  font-size: 18px;
}

.search-icon {
  position: relative;
  z-index: 100;
  margin: 5px 5px 0px 0px;
  background: inherit;
  border-radius: inherit;
  transition: all 0.25s ease-in 0.25s;
}
</style>
