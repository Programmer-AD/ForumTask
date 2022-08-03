const CopyWebpackPlugin = require("copy-webpack-plugin");

module.exports = {
    entry: "/app.jsx",
    output: {
        path: __dirname + '/../publish/',
        filename: "app.js",
    },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                loader: "babel-loader",
                options: {
                    presets: ["@babel/preset-env", "@babel/preset-react"]
                }
            },
            {
                test: /\.css/,
                use: ["style-loader", "css-loader"],
            }
        ]
    },
    plugins: [
        new CopyWebpackPlugin({
            patterns: [
                { from: '../static' }
            ]
        })
    ]
}