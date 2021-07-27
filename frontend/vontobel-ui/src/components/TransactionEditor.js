import React, { useState } from 'react'
import { Modal, Button } from 'react-bootstrap'
import { TransactionEditorForm } from './TransactionEditorForm'

export const TransactionEditModal = props => {
    const [state, updateState] = useState({
        productType: props.transaction ? props.transaction.productType : 'Food',
        amount: props.transaction ? props.transaction.amount : '',
        currency: props.transaction ? props.transaction.currency : 'CHF',
        recipient: props.transaction ? props.transaction.recipient: '',
        errors: []
    })

    const onSave = e => {
        e.preventDefault()

        var trx = {
            id: props.transaction.id,
            productType: state.productType,
            amount: state.amount,
            currency: state.currency,
            recipient: state.recipient
        }
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(trx)
        };
        
        fetch(process.env.REACT_APP_API_URL+'/api/transaction/'+trx.id, requestOptions)
            .then(() => props.onSave(trx));
    }

    const onFormChange = newState => {
        updateState(newState)
    }

    return <Modal show={props.transaction != null} onHide={props.onClose}>
    <Modal.Header closeButton>
      <Modal.Title>Edit transaction</Modal.Title>
    </Modal.Header>
    <Modal.Body><TransactionEditorForm transaction={props.transaction} onFormChange={onFormChange}/></Modal.Body>
    <Modal.Footer>
      <Button variant="secondary" onClick={props.onClose}>
        Close
      </Button>
      <Button variant="primary" onClick={onSave} disabled={state.errors.length > 0}>
        Save Changes
      </Button>
    </Modal.Footer>
  </Modal>
}