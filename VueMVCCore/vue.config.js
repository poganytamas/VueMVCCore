var Agent = require('agentkeepalive') // windows authentication requires this

const path = require('path')
const root = path.resolve(__dirname, 'src') // vue files are in src folder

module.exports = {    
	publicPath: '/', // this needs to be set, check the node_env here if prod. is different
    outputDir: 'wwwroot/dist', // npm run build puts the files here
    filenameHashing: false, // no hash at the end of file names (cache related, mvc core can also handle the cache)
    configureWebpack: {
        optimization: {
            splitChunks: false // one big file instead of small ones
        },
        resolve: {
            extensions: ['.js', '.vue'], // no need to put .js or .vue in imports
            alias: { // useful when using import
                '@app': `${root}`,
                '@components': `${root}/components`,
                '@views': `${root}/views`,
                '@mixins': `${root}/mixins`,
                '@store': `${root}/store`,
                '@router': `${root}/router`
            }
        }
    },
    devServer: {
        port: 8080,
        public: 'localhost:8080', // this is needed for websocket, otherwise hmr won't work
        proxy: {
            // mvc core css directory
            '/css/*': {
                target: 'http://localhost:55318' // this must be the same port as IIS express
            },
            // mvc core will do the api
            '/api/*': {
                target: 'http://localhost:55318', // this must be the same port as in IIS express
                // the below configuration is needed for windows auth
                changeOrigin: true,
                agent: new Agent({
                    maxSockets: 100,
                    keepAlive: true,
                    maxFreeSockets: 10,
                    keepAliveMsecs: 100000,
                    timeout: 6000000,
                    freeSocketTimeout: 90000 // free socket keepalive for 90 seconds
                }),
                onProxyRes: (proxyRes) => {
                    var key = 'www-authenticate';
                    proxyRes.headers[key] = proxyRes.headers[key] && proxyRes.headers[key].split(',');
                }
            }
        }
    }
}