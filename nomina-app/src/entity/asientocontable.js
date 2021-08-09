import { List, Datagrid, TextField, BooleanField, Create,  SimpleForm, 
    NumberField,ReferenceField, DateField,
    DateInput,TextInput,Show,SimpleShowLayout,NumberInput,BooleanInput   } from 'react-admin';

    const Filters = [
        <NumberInput label="ID" source="id" />,
        <DateInput source="Fecha" />,
        <TextInput label="Descripción"  source="Descripción" />,
        <NumberInput label="Monto" source="Monto" />,
        <BooleanInput source="Contabilizado" />,
        
    ];


export const AsientoContableList = props => (
    <List {...props} filters={Filters} sort={{ field: 'Fecha', order: 'DESC' }}>
        <Datagrid rowClick="show">
            <TextField source="id" />
            <DateField source="Fecha" />
            <TextField label="Descripción" source="Descripcion" />
            <NumberField source="Auxiliar" />
            <ReferenceField label="Cuenta débito" source="Cuentadb" reference="Cuenta"><TextField source="Descripcion" /></ReferenceField>
            <ReferenceField label="Cuenta crédito" source="Cuentacr" reference="Cuenta"><TextField source="Descripcion" /></ReferenceField>
            <NumberField source="Monto" />
            <BooleanField source="Contabilizado" />
            <TextField source="Estado" />
        </Datagrid>
    </List>
);

export const AsientoContableShow = props => (
    <Show {...props}>
        <SimpleShowLayout>
            <TextField source="id" />
            <DateField source="Fecha" />
            <TextField label="Descripción" source="Descripcion" />
            <NumberField source="Auxiliar" />
            <ReferenceField label="Cuenta débito" source="Cuentadb" reference="Cuenta"><TextField source="Descripcion" /></ReferenceField>
            <ReferenceField label="Cuenta crédito" source="Cuentacr" reference="Cuenta"><TextField source="Descripcion" /></ReferenceField>
            <NumberField source="Monto" />
            <BooleanField source="Contabilizado" />
            <TextField source="Estado" />
        </SimpleShowLayout>
    </Show>
);

export const AsientoContableCreate = props => (
    <Create {...props}>
        <SimpleForm>
            <DateInput source="Fecha" />
            <TextInput source="Descripcion" />
        </SimpleForm>
    </Create>
);