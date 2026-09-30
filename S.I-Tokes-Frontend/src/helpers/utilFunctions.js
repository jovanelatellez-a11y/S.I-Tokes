function objectValidate (obj) {
    return Object.values(obj).every(
        value => 
            value !== undefined &&
            value !== null && 
            !(typeof value === 'string' && value.trim() === '')
    )
}

export const utilsFunctions = {
    objectValidate
}