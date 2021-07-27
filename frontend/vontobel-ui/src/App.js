import React, { useState } from 'react';
import { Header } from './components/Header';
import { LastTransactions } from './components/LastTransactions';
import { TransactionAdder } from './components/TransactionAdder';

function App() {

  const [state, updateState] = useState({
    refreshCount: 0
  })

  const onSubmittedTransaction = () => {
    updateState({...state, refreshCount: state.refreshCount+1})
  }

  return (
    <div className="App container-fluid">
      <Header/>
      <div className="container-fluid">
        <div className='row'>
          <div className='col-8'>
            <LastTransactions refreshCount={state.refreshCount}/>
          </div>
          <div className='col-4'>
            <TransactionAdder onSubmittedTransaction={onSubmittedTransaction}/>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
