import * as React from "react";
import { List, Datagrid, TextField, BooleanField, Edit, Create, TextInput, SimpleForm, BooleanInput, NumberField,ReferenceField,NumberInput,ReferenceInput,SelectInput   } from 'react-admin';

export const DeduccionList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="id" />
            <TextField source="Nombre" />
            <BooleanField source="Automatico" />
            <BooleanField source="Porcentual" />
            <NumberField source="Cantidad" />
            <BooleanField source="Estado" />
        </Datagrid>
    </List>
);

export const DeduccionEdit = props => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput source="Nombre" />
            <BooleanInput source="Automatico" />
            <BooleanInput source="Porcentual" />
            <NumberInput source="Cantidad" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Edit>
);

export const DeduccionCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="Nombre" />
            <BooleanInput source="Automatico" />
            <BooleanInput source="Porcentual" />
            <NumberInput source="Cantidad" />
            <BooleanInput source="Estado" />
        </SimpleForm>
    </Create>
);
