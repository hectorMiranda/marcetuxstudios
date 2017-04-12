// webpack.config.js  — Webpack 3 with scope hoisting enabled.
// Requires ES2015 modules (import/export) to work; CommonJS require() is exempt.
const webpack = require('webpack');
const path    = require('path');

module.exports = {
  entry: './src/main.ts',
  output: {
    path:     path.resolve(__dirname, 'dist'),
    filename: '[name].[chunkhash].js',
  },
  resolve: { extensions: ['.ts', '.js'] },
  module: {
    rules: [
      { test: /\.ts$/, use: ['awesome-typescript-loader', 'angular2-template-loader'] },
    ],
  },
  plugins: [
    // Scope hoisting — flatten ES module scopes to reduce wrapper overhead.
    new webpack.optimize.ModuleConcatenationPlugin(),
    // Still useful alongside scope hoisting for minification.
    new webpack.optimize.UglifyJsPlugin({ compress: { warnings: false } }),
  ],
};
