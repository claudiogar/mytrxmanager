import React, { useEffect, useState } from 'react'
import { Button, ButtonGroup, ButtonToolbar } from 'react-bootstrap'

export const LastTransactions = props => {
    var [state, updateState] = useState({
        pageIndex: 0,
        pageLength: 10,
        records: [],
        loading: 'notStarted'
    });

    const fetchRecords = async (trxId, limit) => {
        updateState({...state, loading: 'started'});

        try{
            var url = process.env.REACT_APP_API_URL+'/api/transaction?limit='+limit;

            if(trxId){
                url = url + '&startingId='+trxId
            }
    
            const res = await fetch(url, { 
                Method: 'GET',
                Accept: 'application/json'
            });
    
            var newRecords = await res.json();
    
            if(newRecords.length == 0) return; // nothing to do. We reached the boundary.
    
            var newPageIndex = limit > 0 ? state.pageIndex + 1 : state.pageIndex -1
    
            updateState({...state, records: newRecords, loading: 'notStarted', pageIndex: newPageIndex});

        }catch{
            updateState({...state, loading: 'notStarted'});
        }
    }

    const goPreviousPage = () => {
        var firstId = Math.max(...state.records.map(r => r.id))
        fetchRecords(firstId, -1 * state.pageLength)
    }

    const goNextPage = () => {
        var lastId = Math.min(...state.records.map(r => r.id))

        fetchRecords(lastId, state.pageLength)
    }

    useEffect(() => {
        if(state.loading == 'notStarted'){
            fetchRecords(null, state.pageLength)
        }
    }, []);

    return <React.Fragment>
        <div className="text-left">
          <span className="h4">Most recent transactions</span>
        </div>
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Date</th>
              <th scope="col">Product Type</th>
              <th scope="col">Amount</th>
              <th scope="col">Currency</th>
              <th scope="col">Recipient</th>
            </tr>
          </thead>
          <tbody>
                {state.records.map(record =>(
                    <React.Fragment>
                        <tr key={record.id}>
                            <th scope="row">{record.id}</th>
                            <td>{record.date}</td>
                            <td>{record.productType}</td>
                            <td>{record.amount}</td>
                            <td>{record.currency}</td>
                            <td>{record.recipient}</td>
                        </tr>
                    </React.Fragment>
                ))}
        </tbody>
        </table>
        <div className='text-right col-12'>
            <ButtonGroup className="me-2 btn-group-sm" aria-label="Pager">
                <Button onClick={goPreviousPage}>Prev</Button>
                <Button disabled>{state.pageIndex}</Button>
                <Button onClick={goNextPage}>Next</Button>
            </ButtonGroup>
        </div>
    </React.Fragment>
}