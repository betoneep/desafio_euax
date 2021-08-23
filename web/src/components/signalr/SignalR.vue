<template>
  <input type="hidden" />
</template>

<script>
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

export default {
  name: "SignalR",
  props: {
    url: {
      type: String,
      default: ""
    },
    parametro: {
      type: Object,
      default: Object
    },
    nomeEventoRegistro: {
      type: String,
      default: "Registrar"
    },
    nomeEventoReceber: {
      type: String,
      default: ""
    },
    nomeEventoEmit: {
      type: String,
      default: ""
    },
    nomeHub: {
      type: String,
      default: ""
    }
  },
  data() {
    return {
      HubConnection: {}
    };
  },
  computed: {},
  mounted() {
    this.HubConnection = new HubConnectionBuilder()
      .withUrl(this.$props.url + this.$props.nomeHub)
      .withAutomaticReconnect(this.ObterArrayReconexao())
      .configureLogging(LogLevel.Information)
      .build();

    let vm = this;
    this.HubConnection.onreconnected((connectionId) => {
      console.log("Chamou evento de reconexão do signalR. ", connectionId);
      vm.Registrar();
    });

    this.HubConnection.on(this.$props.nomeEventoReceber, (msg) => {
      //console.log("Nova msg signalr", msg);
      this.$emit(this.$props.nomeEventoEmit, msg);
    });
  },
  methods: {
    Iniciar() {
      let vm = this;
      vm.HubConnection.start().then(() => {
        vm.Registrar();
      });
    },
    Registrar() {
      this.HubConnection.invoke(
        this.$props.nomeEventoRegistro,
        this.$props.parametro
      ).catch((err) =>
        console.error("Erro ao registrar SignalR: ", err.toString())
      );
    },
    Fechar() {
      this.HubConnection.stop().then(() => {
        console.log("fechou conexão");
      });
    },
    ObterArrayReconexao() {
      let array = [];
      array.push(0);
      array.push(5000);
      for (let i = 0; i < 3600; i++) {
        array.push(10000);
      }
      return array;
    }
  }
};
</script>
