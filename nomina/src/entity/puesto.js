import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberInput } from 'react-admin';


const postFilters = [
    <NumberInput label="ID" source="id" />,
    <TextInput label="Puesto"  source="puesto" />
];

export const PuestoList = props => (
    <List {...props} filters={postFilters}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="puesto" />
        </Datagrid>
    </List>
);

export const PuestoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="puesto" />
        </SimpleForm>
    </Edit>
);

export const PuestoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="puesto" />
        </SimpleForm>
    </Create>
);