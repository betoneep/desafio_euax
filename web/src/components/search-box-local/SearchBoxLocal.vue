<template>
  <div class="search-box">
    <i class="search-icon fa fa-search"></i>
    <input
      v-model="inputText"
      class="search"
      type="text"
      :placeholder="this.$props.placeHolder"
      @keyup="searchTimeOut"
    />
  </div>
</template>

<script>
export default {
  name: "SearchBox",
  props: {
    searchKey: {
      type: Array,
      default: Array
    },
    dataItems: {
      type: Array,
      default: () => {
        return [];
      }
    },
    dataItemsFiltered: {
      type: Array,
      default: Array
    },
    placeHolder: {
      type: String,
      default: "Busque aqui"
    }
  },
  data() {
    return {
      inputText: "",
      ldataItems: [...this.$props.dataItems],
      ldataItemsTemp: [],
      timer: 0
    };
  },
  mounted() {
    // this.ldataItems = this.$props.dataItems;
    // console.log("Definido data items do search: ", this.ldataItems);
    this.ldataItemsTemp = [...this.$props.dataItems];
    // console.log(
    //   "Temporário inicializado Spread Operator:",
    //   this.ldataItemsTemp
    // );
    this.onSearchFiltered([...this.ldataItems]);
  },
  methods: {
    onSearchFiltered(e) {
      console.log("Component: SearchBox, OnSearchFiltered: ", e);
      this.$emit("on-search-filter", e);
    },
    searchTimeOut() {
      console.log("Enter Timer!");
      if (this.timer) {
        clearTimeout(this.timer);
        this.timer = null;
        console.log("Clear Timer!");
      }
      console.log("Start Timer!");
      this.timer = setTimeout(() => {
        console.log("After Timer!");
        this.objectSearch(this.ldataItems, this.inputText);
      }, 500);
    },
    objectSearch(objectArray, match) {
      if (!match) {
        console.log("Buscar nula!");
        this.onSearchFiltered([...this.ldataItemsTemp]);
        return;
      }
      console.log(
        "Object Search started! ObjectArray:",
        objectArray,
        "Match: ",
        match
      );
      console.log("ObjectArray: ", objectArray);
      console.log("InputText: ", this.inputText);
      console.log("Match: ", match);

      console.log("Objeto 0 do array: ", objectArray[0]);
      let objArray = objectArray[0];
      const keys = Object.keys(objArray);
      console.log("Teste Uppercase1: ", objArray[keys[0]].toUpperCase());
      console.log("Teste Uppercase2: ", match.toUpperCase());

      this.onSearchFiltered(
        objectArray.filter(
          (obj) =>
            obj[keys[0]].toUpperCase().includes(match.toUpperCase()) ||
            obj[keys[1]].toUpperCase().includes(match.toUpperCase())
        )
      );
      console.log(
        "Result: ",
        this.ldataItemsTemp,
        "Result props: ",
        this.$props.dataItems
      );
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
