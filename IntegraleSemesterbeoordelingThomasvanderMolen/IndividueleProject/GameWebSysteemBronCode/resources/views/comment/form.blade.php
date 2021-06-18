<div class="box box-info padding-1">
    <div class="box-body">
        <div class="form-group">
            {{ Form::label('content') }}
            {{ Form::textarea('content', $comment->content, ['class' => 'form-control' . ($errors->has('content') ? ' is-invalid' : ''), 'placeholder' => 'Content']) }}
            @if ($comment->run_id === null)
            {{ Form::hidden('run_id', $run_id) }}
            @endif
            {!! $errors->first('content', '<div class="invalid-feedback">:message</p>') !!}
            @if (auth()->user()->admin)
                <div class="form-group">
                    {{ Form::label('active') }}
                    {{ Form::text('active', $comment->active, ['class' => 'form-control' . ($errors->has('active') ? ' is-invalid' : ''), 'placeholder' => 'active 1 or 0']) }}
                    {!! $errors->first('active', '<div class="invalid-feedback">:message</p>') !!}
                </div>
            @endif
        </div>
    </div>
    <div class="box-footer mt20">
        <button type="submit" class="btn btn-primary">Submit</button>
    </div>
</div>
