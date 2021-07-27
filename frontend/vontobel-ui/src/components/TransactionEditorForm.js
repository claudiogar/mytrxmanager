import React, { useEffect, useState } from 'react'


export const TransactionEditorForm = props => {

    const [state, updateState] = useState({
        productType: props.transaction ? props.transaction.productType : 'Food',
        amount: props.transaction ? props.transaction.amount : '',
        currency: props.transaction ? props.transaction.currency : 'CHF',
        recipient: props.transaction ? props.transaction.recipient : '',
        errors: []
    })
    
    const hasError = name => {
        return state.errors.indexOf(name)>= 0
    }

    const validate = () => {
        var errors = []
        if(!state.productType){
            errors.push("productType")
        }

        if(!isNumeric(state.amount.toString())){
            errors.push('amount')
        }

        if(!state.recipient || state.recipient.length < 3){
            errors.push('recipient')
        }

        errors.sort()
        
        var errorsChanged = errors.length != state.errors.length;
        if(!errorsChanged && errors.length == state.errors.length){
            var i = 0;
            for(i=0; i < errors.length; i++){
                if(errors[i] != state.errors[i]){
                    errorsChanged = true
                    break
                }
            }
        }

        if(errorsChanged){
            var newState = {...state, errors: errors.sort()}
            updateState(newState)
            props.onFormChange(newState)
        }
    }

    useEffect(() =>{
        validate()
    })

    const handleInputChange =(event) => {
        var key = event.target.id
        var value = event.target.value

        var newState = {...state, [key]: value}
        updateState(newState)
        validate()
        props.onFormChange(newState)
      }
    
      function isNumeric(str) {
        if (typeof str != "string") return false // we only process strings!  
        return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
               !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
      }

    return <form>
            <div className="form-group">
                <label htmlFor="productType">Product type</label>
                    <select
                    className={hasError("productType") ? "form-control is-invalid" : "form-control"}
                    id="productType"
                    value={state.productType}
                    onChange={handleInputChange}
                    required>
                        <option>Food</option>
                        <option>Drinks</option>
                        <option>Other</option>
                    </select>
            </div>
            <div className="form-group">
                <label htmlFor="amount">Amount</label>
                <input type="amount"
                    className={hasError("amount") ? "form-control is-invalid" : "form-control"}
                    id="amount" placeholder="Amount"
                value={state.amount}
                onChange={handleInputChange}
                required/>
            </div>
            <div className="form-group">
                <label htmlFor="currency">Currency</label>
                    <select
                    className={hasError("currency") ? "form-control is-invalid" : "form-control"}
                    id="currency"
                    value={state.currency}
                    onChange={handleInputChange}
                    required>
                        <option>CHF</option>
                        <option>EUR</option>
                        <option>USD</option>
                        <option>GBP</option>
                        <option>AUD</option>
                    </select>
            </div>
            <div className="form-group">
                <label htmlFor="recipient">Recipient</label>
                <input type="recipient"
                className={hasError("recipient") ? "form-control is-invalid" : "form-control"}
                id="recipient"
                placeholder="Recipient"
                value={state.recipient}
                onChange={handleInputChange}
                required/>
            </div>
        </form>
}