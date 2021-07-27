import React, { useEffect, useState } from 'react'

export const TransactionAdder = props => {

    const [state, updateState] = useState({
        productType: 'Food',
        amount: '',
        currency: 'CHF',
        recipient: '',
        errors: []
    })

    const hasError = name => {
        return state.errors.indexOf(name)>= 0
    }

    const submitForm = e => {
        e.preventDefault()
        if(state.errors.length == 0){
            props.onSubmit({
                productType: state.productType,
                amount: state.amount,
                currency: state.currency,
                recipient: state.recipient
            })
        }
    }

    const validate = () => {
        var errors = []
        if(!state.productType){
            errors.push("productType")
        }

        if(!isNumeric(state.amount)){
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
            updateState({...state, errors: errors.sort()})
        }
    }

    useEffect(() =>{
        validate()
    })

    const handleInputChange =(event) => {
        var key = event.target.id
        var value = event.target.value
        updateState({...state, [key]: value})
        validate()
      }
    
      function isNumeric(str) {
        if (typeof str != "string") return false // we only process strings!  
        return !isNaN(str) && // use type coercion to parse the _entirety_ of the string (`parseFloat` alone does not do this)...
               !isNaN(parseFloat(str)) // ...and ensure strings of whitespace fail
      }
      

    return <React.Fragment>
        <div className='panel panel-default'>
            <div className='panel-heading'><h4 className='panel-title'>Add transaction</h4></div>
            <div className='panel-body'>
            <form>
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
                <br/>
                <div className="form-group">
                    <button
                    type="submit"
                    className="btn btn-primary"
                    onClick={submitForm}
                    disabled={state.errors.length > 0}
                    >Submit</button>
                </div>
                </form>
            </div>
        </div>
    </React.Fragment>
}