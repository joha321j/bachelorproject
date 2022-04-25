module.exports = {
    mode: 'jit',
    content: [
        '**/Components/*.razor',
        '**/Pages/*.razor',
        '**/Shared/*.razor',
        '**/wwwroot/index.html',
        '**/App.razor',
        '**/wwwroot/app.css',
    ],
    theme: {

        colors: {
            'UR-logo-text': '#2F2F2F',
            'white': '#FFFFFF',
            'black': '#000000',
            'gray': {
                900: '#0A0A14',
            },
            'primary': {
                900: '#265D82',
                500: '#3E769B',
            },
            'brand': {
                500: '#56A0D3',
                400: '#80B8DE',
                300: '#C2E1F6',
                200: '#D5E7F4',
            },
            'base': {
                500: '#617980',
                400: '#899BA0',
                300: '#B0BCBF',
                200: '#D7DDDF',
            },
            'success': {
                900: '#2A6E12',
                700: '#3C9D19',
                200: '#C5E2BA',
            },
            'warning': {
                900: '#EC6800',
                100: '#FAE8B1',
            },
            'error': {
                900: '#B92E15',
                800: '#D23A1D',
                100: '#FDCABD',
            }
        },
        fontSize: {
            'xs': ['12px', {
                letterSpacing: '0.02em',
                lineHeight: '16px'
            }],
            'sm': ['14px', {
                letterSpacing: '0.02em',
                lineHeight: '20px'
            }],
            'base': ['16px', {
                letterSpacing: '0.02em',
                lineHeight: '24px'
            }],
            'lg': ['20px', {
                letterSpacing: '0.02em',
                lineHeight: '28px'
            }],
            'xl': ['24px', {
                lineHeight: '32px'
            }],
            '2xl': ['32px', {
                lineHeight: '36px'
            }],
            '3xl': ['40px', {
                lineHeight: '48px'
            }],
            '4xl': ['48px', {
                lineHeight: '56px'
            }],
            '5xl': ['60px', {
                lineHeight: '72px'
            }],
        },
        extend: {
            fontFamily: {
                'sans': ['Roboto', 'sans-serif'],
            },
            width: {
                '128': '32rem',
                '160': '40rem',
                '192': '48rem',
                '256': '64rem',
            }
        },
    },
    plugins: [],
}
