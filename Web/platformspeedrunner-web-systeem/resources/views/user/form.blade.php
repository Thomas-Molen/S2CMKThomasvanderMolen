<div class="box box-info padding-1">
    <div class="box-body">

        <div class="form-group">
            {{ Form::label('username') }}
            {{ Form::text('username', $user->username, ['class' => 'form-control' . ($errors->has('username') ? ' is-invalid' : ''), 'placeholder' => 'Username']) }}
            {!! $errors->first('username', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('unique_key') }}
            {{ Form::text('unique_key', $user->unique_key, ['class' => 'form-control' . ($errors->has('unique_key') ? ' is-invalid' : ''), 'placeholder' => 'Unique Key']) }}
            {!! $errors->first('unique_key', '<div class="invalid-feedback">:message</p>') !!}
        </div>
        <div class="form-group">
            {{ Form::label('role_id') }}
            {{ Form::text('role_id', $user->role_id, ['class' => 'form-control' . ($errors->has('role_id') ? ' is-invalid' : ''), 'placeholder' => 'Role']) }}
            {!! $errors->first('role_id', '<div class="invalid-feedback">:message</p>') !!}
        </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
