var path = require('path');

module.exports = {
    context: path.join(__dirname, 'Content'),
    entry: {
        server: './server',
        client: './client'
    },
    output: {
        path: path.join(__dirname, 'build'),
        filename: '[name].bundle.js'
    },
    module: {
        loaders: [
          // Transform JSX in .jsx files
          //{ test: /\.jsx$/, loader: 'jsx-loader?harmony' }
          {
              test: /.jsx?$/,
              loader: 'babel-loader',
              exclude: /node_modules/,
              query: {
                  presets: ['es2015', 'react']
              }
          },
          {
              test: /\.css$/,
              loader: "style-loader!css-loader"
          },
          //{
          //    test: /\.png$/,
          //    loader: "url-loader?limit=100000"
          //},
          //{
          //    test: /\.jpg$/,
          //    loader: "file-loader"
          //},
          {
              test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
              loader: 'url?limit=10000&mimetype=application/font-woff'
          },
          {
              test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,
              loader: 'url?limit=10000&mimetype=application/octet-stream'
          },
          {
              test: /\.eot(\?v=\d+\.\d+\.\d+)?$/,
              loader: 'file'
          },
          {
              test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
              loader: 'url?limit=10000&mimetype=image/svg+xml'
          }
        ],
    },
    resolve: {
        // Allow require('./blah') to require blah.jsx
        extensions: ['', '.js', '.jsx']
    },
    externals: {
        // Use external version of React (from CDN for client-side, or
        // bundled with ReactJS.NET for server-side)
        react: 'React'
    }
};