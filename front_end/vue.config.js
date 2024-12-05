const { defineConfig } = require("@vue/cli-service");
module.exports = defineConfig({
  transpileDependencies: true,
});

module.exports = {
  devServer: {
    // proxy: "http://localhost:5078/",
    proxy: "http://localhost:5078/",
  },
};
/*
export default defineConfig({
  server:{
    port:8080,
  proxy: {
    "/api":{
      target: "http://localhost:5078/api/Task/",
      changeOrigin: true,
      rewrite: (path)=> path.replace(/^\api/, ""),
    }
  }   },
})*/
