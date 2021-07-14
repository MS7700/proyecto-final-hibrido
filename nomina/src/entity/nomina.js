import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput } from 'react-admin';


export const NominaList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="nomina" />
        </Datagrid>
    </List>
);

export const NominaEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="nomina" />
        </SimpleForm>
    </Edit>
);

export const NominaCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="nomina" />
        </SimpleForm>
    </Create>
);