import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput } from 'react-admin';

export const DeduccionList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Nombre" />
            <BooleanField source="DependeSalario" />
            <BooleanField source="Estado" />
        </Datagrid>
    </List>
);

export const DeduccionEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="Nombre" />
            <BooleanInput source="DependeSalario" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Edit>
);

export const DeduccionCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Nombre" />
            <BooleanInput source="DependeSalario" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Create>
);