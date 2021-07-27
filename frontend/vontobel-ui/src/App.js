import React from 'react';
import { Header } from './components/Header';
import { LastTransactions } from './components/LastTransactions';
import { TransactionAdder } from './components/TransactionAdder';

function App() {
  const submitTransaction = trx => {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(trx)
  };
  fetch(process.env.REACT_APP_API_URL+'/api/transaction', requestOptions)
      .then(response => response.json())

  }

  return (
    <div className="App container-fluid">
      <Header/>
      <div className="container-fluid">
        <div className='row'>
          <div className='col-8'>
            <LastTransactions />
          </div>
          <div className='col-4'>
            <TransactionAdder onSubmit={submitTransaction}/>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
