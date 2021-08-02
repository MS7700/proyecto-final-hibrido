import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput } from 'react-admin';


export const DepartamentoList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Descripcion" />
        </Datagrid>
    </List>
);

export const DepartamentoEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled source="id" />
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Edit>
);

export const DepartamentoCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Create>
);