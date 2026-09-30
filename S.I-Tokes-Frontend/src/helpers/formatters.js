const formatCurrency = (key, currency) => {
    return new Intl.NumberFormat('es-NI', {
        style: 'currency',
        currency: currency == 'USD' ? 'USD' : 'NIO',
    }).format(key)
}

const formatDate =  (dataString) => {
    if (!dataString) {
        return 'xx/xx/xxxx'
    } else {
        const date = new Date(dataString)
        return date.toLocaleDateString('es-MX', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        })
    }
}

export const formatters = {
    formatCurrency,
    formatDate
}