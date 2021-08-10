import { useNotify, useRedirect, fetchStart, fetchEnd, Button, TopToolbar, DeleteButton   } from 'react-admin';

import { useState } from 'react';
import { useDispatch } from 'react-redux';

import SendIcon from '@material-ui/icons/Send';
import { makeStyles } from '@material-ui/core/styles';

export const EnviarDeleteActions = ({basePath, data, resource }) => (
    <TopToolbar>
        <EnviarButton record={data} />
        <DeleteButton basePath={basePath} record={data} resource={resource} />
    </TopToolbar>
);

const useStyles = makeStyles({
    button: {
        fontWeight: 'bold',
        color: 'green',
        // This is JSS syntax to target a deeper element using css selector, here the svg icon for this button
        '& svg': { color: 'green' }
    },
});

export const EnviarButton = ({ record }) => {
    const classes = useStyles();
    const dispatch = useDispatch();
    const redirect = useRedirect();
    const notify = useNotify();
    const [loading, setLoading] = useState(false);
    const handleClick = () => {
        setLoading(true);
        dispatch(fetchStart()); // start the global loading indicator 
        const Record = { ...record };
        fetch(`https://localhost:44340/AsientoContable(${record.id})/Contabilidad.EnviarAsiento`, { method: 'POST', body: Record })
            .then(() => {
                notify('Asiento enviado a contabilidad');
                redirect('/AsientoContable');
            })
            .catch((e) => {
                notify('Error: El asiento no pudo ser enviado correctamente', 'warning')
            })
            .finally(() => {
                setLoading(false);
                dispatch(fetchEnd()); // stop the global loading indicator
            });
    };
    return (<Button children={<SendIcon />} className={classes.button}  label="Enviar" onClick={handleClick} disabled={loading} />)
};