import React, { useState } from 'react'
import { TransactionEditorForm } from './TransactionEditorForm'

export const TransactionAdder = props => {
    const [state, updateState] = useState({
        productType: 'Food',
        amount: '',
        currency: 'CHF',
        recipient: '',
        errors: []
    })

    const submitForm = e => {
        e.preventDefault()
        if(state.errors.length == 0){
            var trx = {
                productType: state.productType,
                amount: state.amount,
                currency: state.currency,
                recipient: state.recipient
            }

            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(trx)
            };
            fetch(process.env.REACT_APP_API_URL+'/api/transaction', requestOptions)
                .then(response => response.json())
                .then(() => props.onSubmittedTransaction());
        }
    }

    const onFormChange = newState => {
        updateState(newState)
    }

    return <React.Fragment>
        <div className='panel panel-default'>
            <div className='panel-heading'><h4 className='panel-title'>Add transaction</h4></div>
            <div className='panel-body'>
                <TransactionEditorForm onFormChange={onFormChange}/>
                <br/>
                <div className="form-group">
                    <button
                    type="submit"
                    className="btn btn-primary"
                    onClick={submitForm}
                    disabled={state.errors.length > 0}>Submit</button>
                </div>
            </div>
        </div>
    </React.Fragment>
}