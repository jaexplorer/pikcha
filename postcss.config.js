const tailwindcss = require('tailwindcss');
const purgecss = require('@fullhuman/postcss-purgecss');
const cssnano = require('cssnano');

class TailwindExtractor {
    static extract(content) {
        return content.match(/[A-Za-z0-9-_:\/]+/g) || [];
    }
}

module.exports = {
    plugins: [
        tailwindcss('./tailwind.config.js'),
        process.env.NODE_ENV === "production" && cssnano({
            preset: 'default',
        }),
        process.env.NODE_ENV === "production" && purgecss({
            content: ['./src/**/*.js'],
            css: ['./src/**/*.css'],
            extractors: [
                {
                    extractor: TailwindExtractor,
                    extensions: ['html', 'js', 'css']
                }
            ],
            whitelist: ['html', 'body'],
            whitelistPatterns: [/alert$/, /btn$/],
            whitelistPatternsChildren: [/alert$/, /btn$/],
        }),
        require('autoprefixer'),
    ],
};
